using MetersSender.Common;
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
        private readonly IDateTimeProvider _dateTimeProvider;
        public PostgreSqlDatabaseRepository(MetersSenderDbContext dbContext,
            IDateTimeProvider dateTimeProvider) 
        { 
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
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

        public async Task AddMeterReadingAsync(long meterId, decimal readingValue)
        {
            await _dbContext.Readings.AddAsync(new Reading
            {
                MeterId = meterId,
                Value = readingValue,
                Request = new Request
                {
                    SendingDateTimeUtc = _dateTimeProvider.UtcNow,
                }
            });
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

        public async Task<List<ReadingModel>> GetMeterReadingsHistoryAsync(long meterId)
        {
            var readings = await _dbContext.Readings
                .Include(_ => _.Request)
                .Where(_ => _.MeterId == meterId)
                .ToListAsync();

            return readings.Select(_ => new ReadingModel
            {
                Value = _.Value,
                SendingDateTime = _.Request.SendingDateTimeUtc
            })
            .OrderByDescending(_ => _.SendingDateTime)
            .ThenByDescending(_ => _.Value)
            .ToList();
        }

        public async Task<List<MeterModel>> GetMetersWithLastReadingsAsync(long houseId)
        {
            var result = new List<MeterModel>();

            var meters = await _dbContext.Meters
                .Include(_ => _.Readings)
                .ThenInclude(_ => _.Request)
                .Where(_ => _.HouseId == houseId)
                .ToListAsync();

            foreach (var meter in meters)
            {
                var meterModel = new MeterModel
                {
                    Id = meter.MeterId.ToString(),
                    Name = meter.Name,
                    RecepientMeterId = meter.RecepientMeterId,
                    SourceMeterId = meter.SourceMeterId
                };

                var lastReading = meter.Readings
                    .OrderByDescending(_ => _.Request.SendingDateTimeUtc)
                    .ThenByDescending(_ => _.Value)
                    .FirstOrDefault();

                if (lastReading != null)
                {
                    meterModel.ReadingValue = lastReading.Value;
                    meterModel.ReadingSendingDateTime = lastReading.Request.SendingDateTimeUtc;
                }

                result.Add(meterModel);
            }

            return result;
        }
    }
}
