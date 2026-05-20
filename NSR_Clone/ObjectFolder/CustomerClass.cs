using System;
using System.Collections.Generic;
using System.Text;

namespace NSR_Clone.ObjectFolder
{
    public class CustomerObject
    {
        public string Name { get; set; } = string.Empty;
        public int CVR { get; set; }
        public int Id { get; set; }
        public int DbId { get; set; }//Related to SQL preparation
        public CustomerObject() { } //Related to SQL preparation

        public CustomerObject(string name, int cvr, int id)
        {
            Name = name;
            CVR = cvr;
            Id = id;
        }
    }

    public class CustomerData
    {
        public string NAME { get; set; }
        public int CVR { get; set; }
        public int ID { get; set; }
    }
}
