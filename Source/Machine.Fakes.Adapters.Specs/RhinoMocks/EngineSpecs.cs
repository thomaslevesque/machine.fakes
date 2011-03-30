using System;
using System.ComponentModel.Design;
using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Fakes.Internal;
using Machine.Specifications;

namespace Machine.Fakes.Adapters.Specs.RhinoMocks
{
    [Subject(typeof (RhinoFakeEngine))]
    [Tags("Rhinomocks")]
    public class AfterInitializingANewFakeCurrentEngine : WithCurrentEngine<RhinoFakeEngine>
    {
        static IServiceContainer _fake;

        Because of = () => _fake = FakeEngineGateway.Fake<IServiceContainer>();

        It should_be_able_to_create_an_instance = () => _fake.ShouldNotBeNull();
    }

    [Subject(typeof (RhinoFakeEngine))]
    [Tags("Verifying a mock (without inline constaints)", "Rhinomocks")]
    public class Given_that_a_call_was_not_expected_to_happen_but_happened_when_verifying : WithCurrentEngine<RhinoFakeEngine>
    {
        static Exception _exception;
        static IServiceContainer _fake;

        Establish context = () =>
        {
            _fake = FakeEngineGateway.Fake<IServiceContainer>();
            _fake.RemoveService(null);
        };

        Because of = () => _exception = Catch.Exception(() => _fake.WasNotToldTo(f => f.RemoveService(null)));

        It should_have_thrown_an_exception = () => _exception.ShouldNotBeNull();
    }

    [Subject(typeof (RhinoFakeEngine))]
    [Tags("Verifying a mock (without inline constaints)", "Rhinomocks")]
    public class Given_that_a_call_was_not_expected_to_happen_and_did_not_happened_when_verifying :
        WithCurrentEngine<RhinoFakeEngine>
    {
        static Exception _exception;
        static IServiceContainer _fake;

        Establish context = () => _fake = FakeEngineGateway.Fake<IServiceContainer>();

        Because of = () => _exception = Catch.Exception(() => _fake.WasNotToldTo(f => f.RemoveService(null)));

        It should_not_have_thrown_an_exception = () => _exception.ShouldBeNull();
    }
}