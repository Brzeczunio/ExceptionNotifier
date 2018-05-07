using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Queris.ExceptionNotifier.Common.Entities;

namespace Queris.ExceptionNotifier.Common.UnitTests.Entities
{
    [TestFixture]
    [Category("Queris.ExceptionNotifier.Common.UnitTests")]
    public class FieldsContainerTests
    {
        private FieldsContainer _fieldsContainer;
        private const string FieldName = "Message";
        private const string Value = "Exception";
        private List<FieldInfo> _expected;

        [OneTimeSetUp]
        public void Init()
        {
            _fieldsContainer = new FieldsContainer();
            _fieldsContainer.Add(FieldName, Value);

            _expected = new List<FieldInfo> { new FieldInfo { Name = FieldName, Value = Value } };
        }

        [Test]
        [Category("FieldsContainerTests.FieldsContainer.UnitTests")]
        public void FieldsContainer_CreateObject_ReturnNewObject()
        {
            var fieldsContainer = new FieldsContainer();

            fieldsContainer.Fields.Should().BeEmpty();
        }

        [Test]
        [Category("FieldsContainerTests.Add.UnitTests")]
        public void Add_AddField_ReturnNewField()
        {
            _fieldsContainer.Fields.Should().BeEquivalentTo(_expected);
        }

        [Test]
        [Category("FieldsContainerTests.GetValue.UnitTests")]
        public void GetValue_Get_ReturnValueField()
        {
            _fieldsContainer.GetValue(FieldName).Should().Match(Value);
        }

        [Test]
        [Category("FieldsContainerTests.Clear.UnitTests")]
        public void Clear_ClearList_ReturnEmptyList()
        {
            var fieldsContainer = new FieldsContainer();

            fieldsContainer.Add(FieldName, Value);

            fieldsContainer.Clear();

            fieldsContainer.Fields.Should().BeEmpty();
        }

        [Test]
        [Category("FieldsContainerTests.Fields.UnitTests")]
        public void Fields_GetFields_ReturnFieldsList()
        {
            var fieldsContainer = new FieldsContainer();

            fieldsContainer.Add(FieldName, Value);

            fieldsContainer.Fields.Should().BeEquivalentTo(_expected);
        }

        [Test]
        [Category("FieldsContainerTests.Replace.UnitTests")]
        public void Replace_ReplaceFields_ReturnReplacedFieldsList()
        {
            var fieldsContainer = new FieldsContainer();
            fieldsContainer.Add(FieldName, Value);
            var expected = new List<FieldInfo> { new FieldInfo { Name = "Exception", Value = "Critical" } };

            fieldsContainer.Replace(expected);

            fieldsContainer.Fields.Should().BeEquivalentTo(expected);
        }
    }
}
