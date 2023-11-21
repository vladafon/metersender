using MetersSender.Common.Models;
using MetersSender.DataAccess.Database;
using MetersSender.DataAccess.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetersSender.DataAccess.Repository
{
    ///<inheritdoc/>
    public class PostgreSqlDatabaseRepository : IDatabaseRepository
    {
        private readonly MetersSenderDbContext _dbContext;
        public PostgreSqlDatabaseRepository(MetersSenderDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        ///<inheritdoc/>
        public async Task AddHouseAsync(HouseModel houseModel, long sourceServiceId, long recepientServiceId)
        {
            var house = new House
            {
                Name = houseModel.Name,
                RecepientServiceId = recepientServiceId,
                SourceServiceId = sourceServiceId
            };

            await _dbContext.Houses.AddAsync(house);
            await _dbContext.SaveChangesAsync();

            foreach(var meterModel in houseModel.Meters)
            {
                var meter = new Meter
                {
                    HouseId = house.HouseId,
                    Name = meterModel.Name,
                    RecepientMeterId = meterModel.RecepientMeterId,
                    SourceMeterId = meterModel.SourceMeterId
                };

                await _dbContext.Meters.AddAsync(meter);
            }

            await _dbContext.SaveChangesAsync();
        }

        ///<inheritdoc/>
        public async Task<long> AddServiceAsync(ServiceModel serviceModel)
        {
            if (await _dbContext.Services.AnyAsync(_ => _.Name == serviceModel.Name))
            {
                return await _dbContext.Services
                    .Where(_ => _.Name == serviceModel.Name)
                    .Select(_ => _.Serviced)
                    .FirstAsync();
            }

            var model = new Service
            {
                Name = serviceModel.Name
            };

            await _dbContext.Services.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model.Serviced;
        }

        ///<inheritdoc/>
        public async Task<List<HouseModel>> GetHousesWitMetersAsync()
        {
            var result = new List<HouseModel>();
            
            var houses = _dbContext.Houses.Include(_ =>_.SourceService).Include(_ => _.RecepientService).ToList();

            foreach (var house in houses)
            {
                var metersModels = new List<MeterModel>();

                var meters = _dbContext.Meters
                    .Where(_ => _.HouseId == house.HouseId).ToList();

                foreach (var meter in meters)
                {
                    var meterModel = new MeterModel
                    {
                        Id = meter.MeterId.ToString(),
                        Name = meter.Name,
                        RecepientMeterId = meter.RecepientMeterId,
                        SourceMeterId = meter.SourceMeterId
                    };

                    metersModels.Add(meterModel);
                }

                var houseModel = new HouseModel
                {
                    Id = house.HouseId,
                    Name = house.Name,
                    Meters = metersModels,
                    RecepientServiceName = house.RecepientService.Name,
                    SourceServiceName = house.SourceService.Name
                };

                result.Add(houseModel);
            }

            return result;
        }
    }
}
