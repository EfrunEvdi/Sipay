using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenBookIDIsNotGreaterThenZero_Validator_ShouldBeReturnError()
        {
            // Arrange
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.BookID = 0;

            // Act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenBookIDIsGreaterThenZero__Validator_ShouldNotReturnError()
        {
            // Arrange
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.BookID = 1;

            // Act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().Be(0);
        }

    }
}
