namespace BarberBoss.Application.UseCases.Billings.Reports.Pdf;

public interface IGenerateBillingsReportPdfUseCase
{
    public Task<byte[]> Execute(DateOnly month);
}
