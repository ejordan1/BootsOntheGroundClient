using System;

namespace AssemblyCSharp
{
	public class Receiver
	{
		public Receiver ()
		{
		}
	}
}
//DO I NEED TO EXIST? ;(



//this is going to have a method that receives parameters UnitModel _receivingUnit, Unitmodel sendingUnit, and then a struct that is the thing that will happen.
//it will then have logic that determines what will happen from there, and could create a really specific command for what is going to happen.
//this logic could also go in the command itself, and then the sending unit could just create a command in the receiving unit's processor. For example:
//buff command. that command would then execute, and in the logic of the command it would check the unit's status and stuff to determine whether it would execute. 
//there is a very simple reason to have a manager: it saves commands from having to exist: if it is determined that the unti will not receive the change,
//it does nothing and the unit does not have to receive the command. If this is not important, there could be a possible benefit that the unit receives the command:
//it could be logged in the eventList from the command processor as an attack that did nothing. This is also not necessary. 