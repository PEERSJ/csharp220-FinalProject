using PartDB;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PartRepository
{
    public class PartModel
    {
        public int Id { get; set; }
        public string Part { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Type { get; set; }
        public int QOH { get; set; }
        public decimal? TotalValue { get; set; }
        public string Note { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class PartRepository
    {
        public PartModel Add(PartModel partModel)
        {
            var partDb = ToDbModel(partModel);

            DatabaseManager.Instance.Parts.Add(partDb);
            DatabaseManager.Instance.SaveChanges();

            partModel = new PartModel
            {
                Id = partDb.PartId,
                Part = partDb.PartNumber,
                Description = partDb.PartDescription,
                Cost = partDb.PartCost,
                Type = partDb.PartType,
                QOH = partDb.PartQOH,
                TotalValue = partDb.PartTotalValue,
                Note = partDb.PartNote,
                LastUpdated = partDb.LastUpdatedOn
            };
            return partModel;
        }

        public List<PartModel> GetAll()
        {
            // Use .Select() to map the database part to PartsModel
            var items = DatabaseManager.Instance.Parts
              .Select(t => new PartModel
              {
                  Id = t.PartId,
                  Part = t.PartNumber,
                  Description = t.PartDescription,
                  Cost = t.PartCost,
                  Type = t.PartType,
                  QOH = t.PartQOH,
                  TotalValue = t.PartTotalValue,
                  Note = t.PartNote,
                  LastUpdated = t.LastUpdatedOn

              }).ToList();

            return items;
        }

        public bool Update(PartModel partModel)
        {
            var original = DatabaseManager.Instance.Parts.Find(partModel.Id);

            if (original != null)
            {
                DatabaseManager.Instance.Entry(original).CurrentValues.SetValues(ToDbModel(partModel));
                DatabaseManager.Instance.SaveChanges();
            }

            return false;
        }

        public bool Remove(int partId)
        {
            var items = DatabaseManager.Instance.Parts
                                .Where(t => t.PartId == partId);

            if (items.Count() == 0)
            {
                return false;
            }

            DatabaseManager.Instance.Parts.Remove(items.First());
            DatabaseManager.Instance.SaveChanges();

            return true;
        }

        private Part ToDbModel(PartModel partModel)
        {
            var partDb = new Part
            {
                PartId = partModel.Id,
                PartNumber = partModel.Part,
                PartDescription = partModel.Description,
                PartCost = partModel.Cost,
                PartType = partModel.Type,
                PartQOH = partModel.QOH,
                PartTotalValue = partModel.TotalValue,
                PartNote = partModel.Note,
                LastUpdatedOn = partModel.LastUpdated
            };

            return partDb;
        }
    }
}
