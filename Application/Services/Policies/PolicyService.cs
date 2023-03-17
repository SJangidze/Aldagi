using Application.Services.Policies.Models;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services.Policies;

public class PolicyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Policy> _genericRepository;

    public PolicyService(IUnitOfWork unitOfWork, IGenericRepository<Policy> genericRepository)
    {
        _unitOfWork= unitOfWork;
        _genericRepository= genericRepository;
    }

    public async Task<IEnumerable<PolicyDTO>> GetMonthPolicies(int year, int month)
    {
        return await _genericRepository.CustomQuery<PolicyDTO>("SELECT p.FROMDATE, a.CreatedDate, p.ID\r\n" +
            "FROM dbo.POLICY as p\r\n" +
            "INNER JOIN Payment.AllocatedAmounts as a\r\n" +
            "ON p.ID = a.Policy\r\n" +
            "WHERE YEAR(p.FROMDATE) = YEAR(a.CreatedDate)\r\n\t" +
            "AND MONTH(p.FROMDATE) = MONTH(a.CreatedDate)\r\n\t" +
            $"AND MONTH(p.FROMDATE) = {month}\r\n\t" +
            $"AND YEAR(p.FROMDATE) = {year}");
    }

    public async Task<IEnumerable<PolicyDTO>> GetQuarterPolicies(int year, int quarter)
    {
        return await _genericRepository.CustomQuery<PolicyDTO>("SELECT p.FROMDATE, a.CreatedDate, p.ID\r\n" +
            "FROM dbo.POLICY as p\r\n" +
            "INNER JOIN Payment.AllocatedAmounts as a\r\n" +
            "ON p.ID = a.Policy\r\n" +
            "WHERE YEAR(p.FROMDATE) = YEAR(a.CreatedDate)\r\n\t" +
            $"AND YEAR(p.FROMDATE) = {year}\r\n\t" +
            $"AND DATEPART(QUARTER, p.FROMDATE) = {quarter}\r\n\t" +
            $"AND DATEPART(QUARTER, a.CreatedDate) = {quarter}");
    }

    private IQueryable<Policy> GetQuery()
    {
        return _unitOfWork.Query<Policy>().Where(x => x.DeletedAt == null);
    }
}
