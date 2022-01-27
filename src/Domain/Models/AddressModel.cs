using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AddressModel : BaseModel
    {
        private string _fullName;

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string _postalCode;

        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }

        private string _state;

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _district;

        public string District
        {
            get { return _district; }
            set { _district = value; }
        }

        private string _street;

        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }

        private string _addressNumber;

        public string AddressNumber
        {
            get { return _addressNumber; }
            set { _addressNumber = value; }
        }

        private string _additional;

        public string Additional
        {
            get { return _additional; }
            set { _additional = value; }
        }

        private string _notes;

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
        private Guid _userId;

        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }


    }
}
