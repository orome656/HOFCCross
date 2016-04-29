using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.Model;

namespace HOFCCross.Service
{
    class MockService : IService
    {
        public List<Actu> GetActu()
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

        public List<Match> GetCalendrier()
        {
            throw new NotImplementedException();
        }
    }
}
