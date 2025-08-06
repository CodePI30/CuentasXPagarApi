using CuentasXPagar.data.DbContextSqlServer;
using CuentasXPagar.data.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuentasXPagar.api.BulkUpdate
{
    public class BulkUpdateData
    {
        private ApplicationDbContext _dbContext;

        public BulkUpdateData(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DocumentoPagar>> BulkUpdateDocuments(List<string> documentsId, int idContable)
        {
            string formatIdContable = idContable.ToString();

            await _dbContext.DocumentoPago.Where(e => documentsId.Contains(e.numero_documento))
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.asientoId, formatIdContable));

            List<DocumentoPagar> updatedRecords = await _dbContext.DocumentoPago
                .Where(e => documentsId.Contains(e.numero_documento))
                .ToListAsync();

            return updatedRecords;
        }
    }
}
