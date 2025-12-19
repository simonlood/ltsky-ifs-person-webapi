using System.Reflection;
using AutoMapper;
using Xunit;

namespace IFS.Person.WebApi.Test.UnitTests
{
    public class MappingConfigurationsTests
    {
        [Fact]
        public void WhenProfilesAreConfigured_ItShouldNotThrowException()
        {
            // Arrange
            var config = new MapperConfiguration(configuration => { configuration.AddMaps(typeof(Program).GetTypeInfo().Assembly); });

            // Assert
            config.AssertConfigurationIsValid();
        }
    }
}