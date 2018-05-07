using System;
using FluentAssertions;
using NUnit.Framework;
using Queris.ExceptionNotifier.Common.Entities;

namespace Queris.ExceptionNotifier.FiltersValidator.UnitTests
{
    [TestFixture]
    [Category("Queris.ExceptionNotifier.FiltersValidator.UnitTests")]
    public class NotificationFiltersValidatorTests
    {
        private Filters _filters;

        [OneTimeSetUp]
        public void Init()
        {
            _filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"Exception", "Critical"}
                    }
                }
            };
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.ToString.UnitTests")]
        public void ToString_WithoutFilters_ReturnNONE()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();

            notificationFiltersValidator.ToString().Should().Match("NONE");
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.ToString.UnitTests")]
        public void ToString_WithFilters_ReturnString()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();

            const string expected = "Id: 0, \r\nFilters: 1: {\r\n	ColumnName: Message, Patterns: [Exception,Critical], MatchPatternOperator: \r\n}\r\n";

            notificationFiltersValidator.SetFilters(_filters);

            notificationFiltersValidator.ToString().Should().Match(expected);
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_FiltersIsNull_ReturnFalse()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();

            notificationFiltersValidator.Validate(null).Should().BeFalse();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageIsNull_ReturnFalse()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();

            notificationFiltersValidator.SetFilters(_filters);

            notificationFiltersValidator.Validate(message).Should().BeFalse();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageNoMatchTheFiltersForDefaultPattern_ReturnFalse()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "Error");

            notificationFiltersValidator.SetFilters(_filters);

            notificationFiltersValidator.Validate(message).Should().BeFalse();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageMatchTheFiltersForDefaultPattern_ReturnTrue()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "Critical");

            notificationFiltersValidator.SetFilters(_filters);

            notificationFiltersValidator.Validate(message).Should().BeTrue();
        }
        
        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageNoMatchTheFiltersForEqualPattern_ReturnFalse()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"0"},
                        MatchPatternOperator = "="
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            notificationFiltersValidator.Validate(message).Should().BeFalse();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageMatchTheFiltersForEqualPattern_ReturnTrue()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"110"},
                        MatchPatternOperator = "="
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            notificationFiltersValidator.Validate(message).Should().BeTrue();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageNoMatchTheFiltersForGreaterPattern_ReturnFalse()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"200"},
                        MatchPatternOperator = ">"
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            notificationFiltersValidator.Validate(message).Should().BeFalse();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageMatchTheFiltersForGreaterPattern_ReturnTrue()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"50"},
                        MatchPatternOperator = ">"
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            notificationFiltersValidator.Validate(message).Should().BeTrue();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageMatchTheFiltersForGreaterPatternWithWrongData_ReturnException()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "text110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"50"},
                        MatchPatternOperator = ">="
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            Action result = () => notificationFiltersValidator.Validate(message);

            result.Should().Throw<FormatException>().WithMessage("Nieprawidłowy format ciągu wejściowego.");
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageNoMatchTheFiltersForGreaterOrEqualPattern_ReturnFalse()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"200"},
                        MatchPatternOperator = ">="
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            notificationFiltersValidator.Validate(message).Should().BeFalse();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageMatchTheFiltersForGreaterOrEqualPattern_ReturnTrue()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"110"},
                        MatchPatternOperator = ">="
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            notificationFiltersValidator.Validate(message).Should().BeTrue();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageMatchTheFiltersForGreaterOrEqualPatternWithWrongData_ReturnException()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "text110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"110"},
                        MatchPatternOperator = ">="
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            Action result = () => notificationFiltersValidator.Validate(message);

            result.Should().Throw<FormatException>().WithMessage("Nieprawidłowy format ciągu wejściowego.");
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageNoMatchTheFiltersForLessPattern_ReturnFalse()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"50"},
                        MatchPatternOperator = "<"
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            notificationFiltersValidator.Validate(message).Should().BeFalse();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageMatchTheFiltersForLessPattern_ReturnTrue()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"200"},
                        MatchPatternOperator = "<"
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            notificationFiltersValidator.Validate(message).Should().BeTrue();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageMatchTheFiltersForLessPatternWithWrongData_ReturnException()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "text110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"200"},
                        MatchPatternOperator = "<"
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            Action result = () => notificationFiltersValidator.Validate(message);

            result.Should().Throw<FormatException>().WithMessage("Nieprawidłowy format ciągu wejściowego.");
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageNoMatchTheFiltersForLessOrEqualPattern_ReturnFalse()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"50"},
                        MatchPatternOperator = "<="
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            notificationFiltersValidator.Validate(message).Should().BeFalse();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageMatchTheFiltersForLessOrEqualPattern_ReturnTrue()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"110"},
                        MatchPatternOperator = "<="
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            notificationFiltersValidator.Validate(message).Should().BeTrue();
        }

        [Test]
        [Category("NotificationFiltersValidatorTests.Validate.UnitTests")]
        public void Validate_MessageMatchTheFiltersForLessOrEqualPatternWithWrongData_ReturnException()
        {
            var notificationFiltersValidator = new NotificationFiltersValidator();
            var message = new FieldsContainer();
            message.Add("Message", "text110");

            var filters = new Filters
            {
                FilterParams = new[]
                {
                    new FilterInfo
                    {
                        ColumnName = "Message",
                        Patterns = new[] {"110"},
                        MatchPatternOperator = "<="
                    }
                }
            };

            notificationFiltersValidator.SetFilters(filters);

            Action result = () => notificationFiltersValidator.Validate(message);

            result.Should().Throw<FormatException>().WithMessage("Nieprawidłowy format ciągu wejściowego.");
        }
    }
}
