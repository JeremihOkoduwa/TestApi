using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repo.repo.mongo
{
    public class GuidService
    {

        private readonly Guid _guidService;

        public GuidService()
        {
            _guidService = Guid.NewGuid();
        }
        public string GetGuidService() => _guidService.ToString("N").Substring(0, 9);
    }
}
