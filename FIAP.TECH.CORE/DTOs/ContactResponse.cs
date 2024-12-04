namespace FIAP.TECH.CORE.DTOs
{
    // Resposta enviada pelo Consumer
    public record ContactResponse(bool Success, List<string> Message);
}
