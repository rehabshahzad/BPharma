using Pharma.Entity.Entities;
using System.Collections.Generic;

namespace Pharma.BLL.Services
{
    public interface IBatchService
    {
        List<Batch> GetAllBatches();

        Batch GetBatchById(int id);

        Batch CreateBatch(
            Batch batch,
            int employeeId
        );

        Batch UpdateBatch(
            int id,
            Batch batch,
            int employeeId
        );
    }
}