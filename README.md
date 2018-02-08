# Machine

Imitation of a Command Line Interface

## Functionality

All interaction happens with an instance of Machine, which contains references to overloaded sub-classes of Command.

There can be multiple Machines, which interact independently with the user.  
Machines always exist within an Environment. Both of which can be created and deleted through commands

Environments without Machines get deleted, as there is nothing to interact with  
The program exits, if there are no more environments

## Commands
### listc
Lists all commands
###

## TO DO
add method: if requires check addComms
if no coincidence call getHelp(). apply to all commands

give exact name for new Machine or Environment

exit command
if null break

add cd -m
