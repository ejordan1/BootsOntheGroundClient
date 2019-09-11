using Data;
using Model;
using Model.Abilities;
using Model.AI;
using Model.Units;
using UniRx;
using Zenject;

namespace Services
{
    public class AIManager : IInitializable {

        private readonly WorldModel _world;
        private readonly CommandProcessor _commandProcessor;

        private readonly IFactory<UnitModel, Ability2> _ability2Factory;
        private readonly IFactory<UnitModel, Ability3> _ability3Factory;
        private readonly IFactory<UnitModel, Ability4> _ability4Factory;
        private readonly IFactory<UnitModel, Ability5> _ability5Factory;
        private readonly IFactory<UnitModel, MovementAbility> _movementAIFactory;


        private readonly AbilityVariablesRegistry _abilityVariablesRegistry;


        public AIManager(WorldModel world,  CommandProcessor commandProcessor,
            AbilityVariablesRegistry abilityVariablesRegistry,
	
            IFactory<UnitModel, Ability2> ability2Factory,
            IFactory<UnitModel, Ability3> ability3Factory,
            IFactory<UnitModel, Ability4> ability4Factory,
            IFactory<UnitModel, Ability5> ability5Factory,
            IFactory<UnitModel, MovementAbility> movementAIFactory
	
        )
	
        {
            _world = world;
            _ability2Factory = ability2Factory;
            _ability3Factory = ability3Factory;
            _ability4Factory = ability4Factory;
            _ability5Factory = ability5Factory;

            _movementAIFactory = movementAIFactory;

            _commandProcessor = commandProcessor;
            _abilityVariablesRegistry = abilityVariablesRegistry;

        }

        public void Initialize(){
		
            /*
            _world
                .Units
                .ObserveAdd ()
                //.Take(1) //attaches AI factory to one unit only
                .Subscribe (eventData => { 

                    if (eventData.Value.unitType == 1){
                        _commandProcessor.AddCommand (_ability2Factory.Create(eventData.Value)) ;   //haaaaaack!
                        _commandProcessor.AddCommand (_ability3Factory.Create(eventData.Value)) ;
                        _commandProcessor.AddCommand (_ability5Factory.Create(eventData.Value)) ;
                    }// These are instead added in the unitspawn
                    if (eventData.Value.unitType == 2){
                        _commandProcessor.AddCommand (_ability2Factory.Create(eventData.Value)) ; 
                        _commandProcessor.AddCommand (_ability4Factory.Create(eventData.Value)) ;
                        _commandProcessor.AddCommand (_ability5Factory.Create(eventData.Value)); 
                    }
                    if (eventData.Value.unitType == 3){
                        _commandProcessor.AddCommand (_ability4Factory.Create(eventData.Value)) ; 
                        _commandProcessor.AddCommand (_ability3Factory.Create(eventData.Value)) ;
                    }
                    if (eventData.Value.unitType == 4){
                        _commandProcessor.AddCommand (_ability2Factory.Create(eventData.Value)) ; 
                        _commandProcessor.AddCommand (_ability5Factory.Create(eventData.Value)) ; ;
                    }

                    _commandProcessor.AddCommand (_movementAIFactory.Create(eventData.Value)) ;

                });
		
		
            */
        }

    }
}
