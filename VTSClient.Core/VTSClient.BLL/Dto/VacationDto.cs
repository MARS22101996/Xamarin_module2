using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTSClient.BLL.Dto
{
    public class VacationDto
    {
        public Guid Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public int VacationStatus { get; set; }

        public int VacationType { get; set; }
    }
}
