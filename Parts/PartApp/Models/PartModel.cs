using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartApp.Models
{
    public class PartModel
    {
        public int PartId { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public decimal PartCost { get; set; }
        public string PartType { get; set; }
        public int PartQOH { get; set; }
        public decimal? PartTotalValue { get; set; }
        public string PartNote { get; set; }
        public DateTime LastUpdatedOn { get; set; }

        public PartRepository.PartModel ToRepositoryModel()
        {
            var repositoryModel = new PartRepository.PartModel
            {
                Id = PartId,
                Part = PartNumber,
                Description = PartDescription,
                Cost = PartCost,
                Type = PartType,
                QOH = PartQOH,
                TotalValue = PartTotalValue,
                Note = PartNote,
                LastUpdated = LastUpdatedOn
            };

            return repositoryModel;
        }

        public static PartModel ToModel(PartRepository.PartModel respositoryModel)
        {
            var partModel = new PartModel
            {
                PartId = respositoryModel.Id,
                PartNumber= respositoryModel.Part,
                PartDescription = respositoryModel.Description,
                PartCost = respositoryModel.Cost,
                PartType = respositoryModel.Type,
                PartQOH = respositoryModel.QOH,
                PartTotalValue = respositoryModel.TotalValue,
                PartNote = respositoryModel.Note,
                LastUpdatedOn = respositoryModel.LastUpdated
            };

            return partModel;
        }

        internal PartModel Clone()
        {
            return (PartModel)MemberwiseClone();
        }

    }
}
