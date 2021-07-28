using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using NUnit.Framework;
using Pandora.Data.Models;
using Pandora.Definitions.Attributes;
using Pandora.Definitions.Interfaces;
using Pandora.Definitions.Operations;

namespace Pandora.Data.Transformers
{
    public static class OperationTests
    {
        [TestCase]
        public static void MappingAnOperationWithNoStatusCodesShouldFail()
        {
            Assert.Throws<Exception>(() => Operation.Map(new OperationWithNoStatusCodes(), "2018-01-01", "MyApi"));
        }

        [TestCase]
        public static void MappingAnOperationWithNoRequestOrResponseObjects()
        {
            var actual = Operation.Map(new OperationWithNoRequestOrResponseObjects(), "2018-01-01", "MyApi");
            Assert.NotNull(actual);
            Assert.AreEqual("2018-01-01", actual.ApiVersion);
            Assert.AreEqual("MyApi", actual.ApiName);
            Assert.AreEqual("OperationWithNoRequestOrResponseObjects", actual.Name);
            Assert.AreEqual("application/json", actual.ContentType);
            Assert.AreEqual("DELETE", actual.Method);
            Assert.AreEqual(false, actual.LongRunning);
            Assert.Null(actual.RequestObject);
            Assert.Null(actual.ResponseObject);
            Assert.AreEqual(1, actual.ExpectedStatusCodes.Count);
            Assert.AreEqual(200, actual.ExpectedStatusCodes.First());
        }

        [TestCase]
        public static void MappingAnOperationWithMultipleStatusCodes()
        {
            var actual = Operation.Map(new OperationWithMultipleStatusCodes(), "2018-01-01", "MyApi");
            Assert.NotNull(actual);
            Assert.AreEqual("2018-01-01", actual.ApiVersion);
            Assert.AreEqual("MyApi", actual.ApiName);
            Assert.AreEqual("OperationWithMultipleStatusCodes", actual.Name);
            Assert.AreEqual("application/json", actual.ContentType);
            Assert.AreEqual("DELETE", actual.Method);
            Assert.AreEqual(false, actual.LongRunning);
            Assert.Null(actual.RequestObject);
            Assert.Null(actual.ResponseObject);
            Assert.AreEqual(2, actual.ExpectedStatusCodes.Count);
            Assert.AreEqual(200, actual.ExpectedStatusCodes.First());
            Assert.AreEqual(201, actual.ExpectedStatusCodes.Skip(1).First());
        }

        [TestCase]
        public static void LongRunningOperationsWithAResponseObjectGetsIgnored()
        {
            var actual = Operation.Map(new LongRunningOperationWithResponseObject(), "2018-01-01", "MyApi");
            Assert.NotNull(actual);
            Assert.AreEqual("2018-01-01", actual.ApiVersion);
            Assert.AreEqual("MyApi", actual.ApiName);
            Assert.AreEqual("LongRunningOperationWithResponseObject", actual.Name);
            Assert.AreEqual("application/json", actual.ContentType);
            Assert.AreEqual("DELETE", actual.Method);
            Assert.AreEqual(true, actual.LongRunning);
            Assert.Null(actual.ResponseObject);
        }

        [TestCase]
        public static void MappingAnOperationWithARequestObject()
        {
            var actual = Operation.Map(new OperationWithRequestObject(), "2018-01-01", "MyApi");
            Assert.NotNull(actual);
            Assert.AreEqual("2018-01-01", actual.ApiVersion);
            Assert.AreEqual("MyApi", actual.ApiName);
            Assert.AreEqual("OperationWithRequestObject", actual.Name);
            Assert.AreEqual("application/json", actual.ContentType);
            Assert.AreEqual("PUT", actual.Method);
            Assert.AreEqual(false, actual.LongRunning);
            Assert.NotNull(actual.RequestObject);
            Assert.Null(actual.ResponseObject);
            Assert.AreEqual(1, actual.ExpectedStatusCodes.Count);
            Assert.AreEqual(200, actual.ExpectedStatusCodes.First());
            Assert.AreEqual("TestObject", actual.RequestObject.GetType().Name);
        }

        [TestCase]
        public static void MappingAnOperationWithAResourceID()
        {
            var actual = Operation.Map(new OperationWithAResourceId(), "2018-01-01", "MyApi");
            Assert.NotNull(actual);
            Assert.AreEqual("2018-01-01", actual.ApiVersion);
            Assert.AreEqual("MyApi", actual.ApiName);
            Assert.AreEqual("OperationWithAResourceId", actual.Name);
            Assert.AreEqual("application/json", actual.ContentType);
            Assert.AreEqual("GET", actual.Method);
            Assert.AreEqual(false, actual.LongRunning);
            Assert.Null(actual.RequestObject);
            Assert.NotNull(actual.ResponseObject);
            Assert.AreEqual(1, actual.ExpectedStatusCodes.Count);
            Assert.AreEqual(200, actual.ExpectedStatusCodes.First());
            Assert.AreEqual("FakeResponseObject", actual.ResponseObject.GetType().Name);
            Assert.AreEqual("FakeResourceId", actual.ResourceIdName);
        }

        [TestCase]
        public static void MappingAnOperationWithAResponseObject()
        {
            var actual = Operation.Map(new OperationWithResponseObject(), "2018-01-01", "MyApi");
            Assert.NotNull(actual);
            Assert.AreEqual("2018-01-01", actual.ApiVersion);
            Assert.AreEqual("MyApi", actual.ApiName);
            Assert.AreEqual("OperationWithResponseObject", actual.Name);
            Assert.AreEqual("application/json", actual.ContentType);
            Assert.AreEqual("GET", actual.Method);
            Assert.AreEqual(false, actual.LongRunning);
            Assert.Null(actual.RequestObject);
            Assert.NotNull(actual.ResponseObject);
            Assert.AreEqual(1, actual.ExpectedStatusCodes.Count);
            Assert.AreEqual(200, actual.ExpectedStatusCodes.First());
            Assert.AreEqual("TestObject", actual.ResponseObject.GetType().Name);
        }

        [TestCase]
        public static void MappingAnOperationWithARequestAndResponseObject()
        {
            var actual = Operation.Map(new OperationWithRequestAndResponseObject(), "2018-01-01", "MyApi");
            Assert.NotNull(actual);
            Assert.AreEqual("2018-01-01", actual.ApiVersion);
            Assert.AreEqual("MyApi", actual.ApiName);
            Assert.AreEqual("OperationWithRequestAndResponseObject", actual.Name);
            Assert.AreEqual("application/json", actual.ContentType);
            Assert.AreEqual("PUT", actual.Method);
            Assert.AreEqual(false, actual.LongRunning);
            Assert.NotNull(actual.RequestObject);
            Assert.NotNull(actual.ResponseObject);
            Assert.AreEqual(1, actual.ExpectedStatusCodes.Count);
            Assert.AreEqual(200, actual.ExpectedStatusCodes.First());
            Assert.AreEqual("TestObject", actual.RequestObject.GetType().Name);
            Assert.AreEqual("TestObject", actual.ResponseObject.GetType().Name);
        }

        [TestCase]
        public static void MappingAnOperationWithASuffix()
        {
            var actual = Operation.Map(new OperationWithASuffix(), "2018-01-01", "MyApi");
            Assert.NotNull(actual);
            Assert.AreEqual("2018-01-01", actual.ApiVersion);
            Assert.AreEqual("MyApi", actual.ApiName);
            Assert.AreEqual("OperationWithASuffix", actual.Name);
            Assert.AreEqual("application/json", actual.ContentType);
            Assert.AreEqual("POST", actual.Method);
            Assert.AreEqual(false, actual.LongRunning);
            Assert.Null(actual.RequestObject);
            Assert.Null(actual.ResponseObject);
            Assert.AreEqual(1, actual.ExpectedStatusCodes.Count);
            Assert.AreEqual(200, actual.ExpectedStatusCodes.First());
            Assert.AreEqual(0, actual.Options.Count);
            Assert.AreEqual("/shutdown", actual.UriSuffix);
        }

        [TestCase]
        public static void MappingAnOperationWithOptions()
        {
            var actual = Operation.Map(new OperationWithOptions(), "2018-01-01", "MyApi");
            Assert.NotNull(actual);
            Assert.AreEqual("2018-01-01", actual.ApiVersion);
            Assert.AreEqual("MyApi", actual.ApiName);
            Assert.AreEqual("OperationWithOptions", actual.Name);
            Assert.AreEqual("application/json", actual.ContentType);
            Assert.AreEqual("POST", actual.Method);
            Assert.AreEqual(false, actual.LongRunning);
            Assert.Null(actual.RequestObject);
            Assert.Null(actual.ResponseObject);
            Assert.AreEqual(1, actual.ExpectedStatusCodes.Count);
            Assert.AreEqual(200, actual.ExpectedStatusCodes.First());
            Assert.AreEqual(2, actual.Options.Count);
            Assert.AreEqual("First", actual.Options[0].Name);
            Assert.AreEqual(true, actual.Options[0].Required);
            Assert.AreEqual(OptionDefinitionType.String, actual.Options[0].Type);
            Assert.AreEqual("first", actual.Options[0].QueryStringName);
            Assert.AreEqual("SecondVal", actual.Options[1].Name);
            Assert.AreEqual(false, actual.Options[1].Required);
            Assert.AreEqual(OptionDefinitionType.Integer, actual.Options[1].Type);
            Assert.AreEqual("second", actual.Options[1].QueryStringName);
        }

        [TestCase]
        public static void MappingAnOperationWithPaginationDetails()
        {
            var actual = Operation.Map(new OperationWithPaginationDetails(), "2018-01-01", "MyApi");
            Assert.NotNull(actual);
            Assert.AreEqual("2018-01-01", actual.ApiVersion);
            Assert.AreEqual("MyApi", actual.ApiName);
            Assert.AreEqual("OperationWithPaginationDetails", actual.Name);
            Assert.AreEqual("application/json; charset=utf-8", actual.ContentType);
            Assert.AreEqual("GET", actual.Method);
            Assert.AreEqual(false, actual.LongRunning);
            Assert.Null(actual.RequestObject);
            Assert.NotNull(actual.ResponseObject);
            Assert.AreEqual(1, actual.ExpectedStatusCodes.Count);
            Assert.AreEqual(200, actual.ExpectedStatusCodes.First());
            Assert.AreEqual(0, actual.Options.Count);
            Assert.AreEqual("FakeNestedType", actual.ResponseObject.GetType().Name);
            Assert.AreEqual("@odata.foobar", actual.FieldContainingPaginationDetails);
        }

        [TestCase]
        public static void MappingAnOperationShouldTrimTheSuffixOfOperation()
        {
            var actual = Operation.Map(new OperationSimpleOperation(), "2018-01-01", "MyApi");
            Assert.NotNull(actual);
            Assert.AreEqual("2018-01-01", actual.ApiVersion);
            Assert.AreEqual("MyApi", actual.ApiName);
            Assert.AreEqual("OperationSimple", actual.Name);
        }

        private class OperationWithNoStatusCodes : ApiOperation
        {
            public string? ContentType()
            {
                return "application/json";
            }

            public IEnumerable<HttpStatusCode> ExpectedStatusCodes()
            {
                return new List<HttpStatusCode>();
            }

            public bool LongRunning()
            {
                return false;
            }

            public HttpMethod Method()
            {
                return HttpMethod.Delete;
            }

            public object? RequestObject()
            {
                return null;
            }

            public object? ResponseObject()
            {
                return null;
            }
            public string? FieldContainingPaginationDetails() => null;

            public Definitions.Interfaces.ResourceID? ResourceId() => null;

            public object? OptionsObject() => null;
            public string? UriSuffix() => null;
        }

        private class OperationWithNoRequestOrResponseObjects : ApiOperation
        {
            public string? ContentType()
            {
                return "application/json";
            }

            public IEnumerable<HttpStatusCode> ExpectedStatusCodes()
            {
                return new List<HttpStatusCode> { HttpStatusCode.OK };
            }

            public bool LongRunning()
            {
                return false;
            }

            public HttpMethod Method()
            {
                return HttpMethod.Delete;
            }

            public object? RequestObject()
            {
                return null;
            }

            public object? ResponseObject()
            {
                return null;
            }
            public string? FieldContainingPaginationDetails() => null;

            public Definitions.Interfaces.ResourceID? ResourceId() => null;
            public object? OptionsObject() => null;
            public string? UriSuffix() => null;
        }

        private class OperationWithMultipleStatusCodes : ApiOperation
        {
            public string? ContentType()
            {
                return "application/json";
            }

            public IEnumerable<HttpStatusCode> ExpectedStatusCodes()
            {
                return new List<HttpStatusCode>
                {
                    HttpStatusCode.OK,
                    HttpStatusCode.Created
                };
            }

            public bool LongRunning()
            {
                return false;
            }

            public HttpMethod Method()
            {
                return HttpMethod.Delete;
            }

            public object? RequestObject()
            {
                return null;
            }

            public Definitions.Interfaces.ResourceID? ResourceId() => null;

            public object? ResponseObject()
            {
                return null;
            }
            public string? FieldContainingPaginationDetails() => null;
            public object? OptionsObject() => null;
            public string? UriSuffix() => null;
        }

        private class LongRunningOperationWithResponseObject : ApiOperation
        {
            public string? ContentType()
            {
                return "application/json";
            }

            public IEnumerable<HttpStatusCode> ExpectedStatusCodes()
            {
                return new List<HttpStatusCode> { HttpStatusCode.OK };
            }

            public bool LongRunning()
            {
                return true;
            }

            public HttpMethod Method()
            {
                return HttpMethod.Delete;
            }

            public object? RequestObject()
            {
                return null;
            }

            public Definitions.Interfaces.ResourceID? ResourceId() => null;

            public object? ResponseObject()
            {
                return new TestObject();
            }
            public string? FieldContainingPaginationDetails() => null;
            public object? OptionsObject() => null;
            public string? UriSuffix() => null;
        }

        private class OperationWithRequestObject : ApiOperation
        {
            public string? ContentType()
            {
                return "application/json";
            }

            public IEnumerable<HttpStatusCode> ExpectedStatusCodes()
            {
                return new List<HttpStatusCode> { HttpStatusCode.OK };
            }

            public bool LongRunning()
            {
                return false;
            }

            public HttpMethod Method()
            {
                return HttpMethod.Put;
            }

            public object? RequestObject()
            {
                return new TestObject();
            }

            public Definitions.Interfaces.ResourceID? ResourceId() => null;

            public object? ResponseObject()
            {
                return null;
            }
            public string? FieldContainingPaginationDetails() => null;
            public object? OptionsObject() => null;
            public string? UriSuffix() => null;
        }

        private class OperationWithResponseObject : ApiOperation
        {
            public string? ContentType()
            {
                return "application/json";
            }

            public IEnumerable<HttpStatusCode> ExpectedStatusCodes()
            {
                return new List<HttpStatusCode> { HttpStatusCode.OK };
            }

            public bool LongRunning()
            {
                return false;
            }

            public HttpMethod Method()
            {
                return HttpMethod.Get;
            }

            public object? RequestObject()
            {
                return null;
            }

            public Definitions.Interfaces.ResourceID? ResourceId() => null;

            public object? ResponseObject()
            {
                return new TestObject();
            }
            public string? FieldContainingPaginationDetails() => null;
            public object? OptionsObject() => null;
            public string? UriSuffix() => null;
        }

        private class OperationWithRequestAndResponseObject : ApiOperation
        {
            public string? ContentType()
            {
                return "application/json";
            }

            public IEnumerable<HttpStatusCode> ExpectedStatusCodes()
            {
                return new List<HttpStatusCode> { HttpStatusCode.OK };
            }

            public bool LongRunning()
            {
                return false;
            }

            public HttpMethod Method()
            {
                return HttpMethod.Put;
            }

            public object? RequestObject()
            {
                return new TestObject();
            }

            public Definitions.Interfaces.ResourceID? ResourceId() => null;

            public object? ResponseObject()
            {
                return new TestObject();
            }
            public string? FieldContainingPaginationDetails() => null;
            public object? OptionsObject() => null;
            public string? UriSuffix() => null;
        }
    }

    public class OperationSimpleOperation : ApiOperation
    {
        public string? ContentType()
        {
            return "application/json";
        }

        public IEnumerable<HttpStatusCode> ExpectedStatusCodes()
        {
            return new List<HttpStatusCode> { HttpStatusCode.OK };
        }

        public bool LongRunning()
        {
            return false;
        }

        public HttpMethod Method()
        {
            return HttpMethod.Put;
        }

        public object? RequestObject()
        {
            return new TestObject();
        }

        public Definitions.Interfaces.ResourceID? ResourceId() => null;

        public object? ResponseObject()
        {
            return new TestObject();
        }
        public string? FieldContainingPaginationDetails() => null;
        public object? OptionsObject() => null;
        public string? UriSuffix() => "/hello";
    }

    public class OperationWithAResourceId : ApiOperation
    {
        public string? ContentType() => "application/json";

        public IEnumerable<HttpStatusCode> ExpectedStatusCodes() => new List<HttpStatusCode>
        {
            HttpStatusCode.OK,
        };

        public string? FieldContainingPaginationDetails() => null;

        public bool LongRunning() => false;

        public HttpMethod Method() => HttpMethod.Get;

        public object? OptionsObject() => null;

        public object? RequestObject() => null;

        public object? ResponseObject() => new FakeResponseObject();

        public Definitions.Interfaces.ResourceID? ResourceId() => new FakeResourceId();

        public string? UriSuffix() => null;

        public class FakeResponseObject
        {
            [JsonPropertyName("foo")]
            public string Foo { get; set; }
        }

        public class FakeResourceId : Definitions.Interfaces.ResourceID
        {
            public string ID()
            {
                return "hello";
            }
        }
    }

    public class OperationWithASuffix : ApiOperation
    {
        public string? ContentType()
        {
            return "application/json";
        }

        public IEnumerable<HttpStatusCode> ExpectedStatusCodes()
        {
            return new List<HttpStatusCode> { HttpStatusCode.OK };
        }

        public bool LongRunning() => false;

        public HttpMethod Method()
        {
            return HttpMethod.Post;
        }

        public object? RequestObject() => null;

        public Definitions.Interfaces.ResourceID? ResourceId() => null;

        public object? ResponseObject() => null;
        public string? FieldContainingPaginationDetails() => null;
        public object? OptionsObject() => null;
        public string? UriSuffix() => "/shutdown";
    }

    public class OperationWithOptions : ApiOperation
    {
        public string? ContentType()
        {
            return "application/json";
        }

        public IEnumerable<HttpStatusCode> ExpectedStatusCodes()
        {
            return new List<HttpStatusCode> { HttpStatusCode.OK };
        }

        public bool LongRunning() => false;

        public HttpMethod Method()
        {
            return HttpMethod.Post;
        }

        public object? RequestObject() => null;

        public Definitions.Interfaces.ResourceID? ResourceId() => null;

        public object? ResponseObject() => null;
        public string? FieldContainingPaginationDetails() => null;
        public object? OptionsObject() => new NestedOptionsObject();
        public string? UriSuffix() => null;

        public class NestedOptionsObject
        {
            [QueryStringName("first")]
            public string First { get; set; }

            [QueryStringName("second")]
            [Optional]
            public int SecondVal { get; set; }
        }
    }

    public class OperationWithPaginationDetails : ListOperation
    {
        public override string? FieldContainingPaginationDetails() => "@odata.foobar";
        public override object NestedItemType() => new FakeNestedType();

        public class FakeNestedType
        {
            [JsonPropertyName("@odata.foobar")]
            public string FooBar { get; set; }
        }
    }

    internal class TestObject
    {
        public bool Whatever { get; set; }
    }
}