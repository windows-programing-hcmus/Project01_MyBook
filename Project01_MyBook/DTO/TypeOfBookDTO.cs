using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.DTO
{
    public class TypeOfBookDTO : INotifyPropertyChanged, ICloneable
    {   
        public string tobID { get; set; }
        public string tobName { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone() as TypeOfBookDTO;
        }
    }
}
