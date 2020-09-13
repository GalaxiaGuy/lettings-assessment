using System;
using System.Threading.Tasks;
using ClearSky.Infrastructure.Services;
using Xunit;

namespace ClearSky.Infrastructure.Tests.Services
{

    public class LettingsServiceTest : LettingsDbContextBaseTest
    {
        private readonly LettingsService _service;

        public LettingsServiceTest() : base()
        {
            _service = new LettingsService(Context);
        }

        [Fact]
        public async Task PageCountIsCorrect()
        {
            var pageCount = await _service.PageCountAsync().ConfigureAwait(false);
            var expectedPageCount = 10;

            Assert.Equal(expectedPageCount, pageCount);

        }
    }
}
