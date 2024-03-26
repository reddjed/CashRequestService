using CashRequestProcessor.Interfaces;
using CashRequestShared.Dto;
using CashRequestShared.Interfaces;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CashRequestProcessor.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RequestRepository> _logger;

        public RequestRepository(IConfiguration configuration, ILogger<RequestRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Guid> CreateRequest(ICreateRequestCommand request)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = await connection.ExecuteScalarAsync<Guid>(
                        "sp_InsertRequest",
                       new
                       {
                           ClientId = request.ClientId,
                           DepartmentAddress = request.DepartmentAddress,
                           Amount = request.Amount,
                           Currency = request.Currency,
                           Status = request.Status.ToString()
                       },
                        commandTimeout: 5,
                        commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing a request to create an aplication");
                throw;
            }

        }

        public async Task<RequestStatusDto> GetRequestStatusByClientIdAndAddress(IGetRequestStatusByClientIdAndDepAddressQuery query)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = await connection.QueryFirstOrDefaultAsync<RequestStatusDto>(
                        "sp_GetRequestStatusByClientIdAndDepAddress",
                       new
                       {
                           ClientId = query.ClientId,
                           DepartmentAddress = query.DepartmentAddress
                       },
                        commandTimeout: 5,
                        commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing a request to create an aplication");
                throw;
            }
        }

        public async Task<RequestStatusDto> GetRequestStatusById(IGetRequestStatusByIdQuery query)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = await connection.QueryFirstOrDefaultAsync<RequestStatusDto>(
                        "sp_GetRequestStatusById",
                       new
                       {
                           RequestId = query.RequestId 
                       },
                        commandTimeout: 5,
                        commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing a request to create an aplication");
                throw;
            }
        }
    }
}
