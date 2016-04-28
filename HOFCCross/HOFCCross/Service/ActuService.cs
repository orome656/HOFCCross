using HOFCCross.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Service
{
    class ActuService
    {
        public List<Actu> GetAll()
        {
            List<Actu> actus = new List<Actu>();
            actus.Add(new Actu()
            {
                Title = "Test 1",
                Content = "Test 1"
            });
            actus.Add(new Actu()
            {
                Title = "Test 1",
                Content = "Test 1"
            });
            actus.Add(new Actu()
            {
                Title = "Test 1",
                Content = "Test 1"
            });
            return actus;
        }
    }
}
