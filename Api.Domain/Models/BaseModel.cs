using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class BaseModel
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        

        private DateTime _createdAt;
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set 
            {
                 _createdAt = value == null ? DateTime.UtcNow : value; 
            }
        }
        
        private DateTime _updateAt;
        public DateTime UpdateAt
        {
            get { return _updateAt; }
            set 
            { _updateAt = value; }
        }
    }
}