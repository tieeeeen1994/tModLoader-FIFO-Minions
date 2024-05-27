# Summary

First In First Out Minions is a mod that will allow minions that were summoned first to also be the one to disappear first when minion slots are filled (First In First Out).
The vanilla behavior is that the last minion summoned will be the first to disappear when the minion slots are filled (Last In First Out).
This mod will change that behavior to First In First Out.

This mod has configuration to ignore items that that have advanced logic for minion summoning.
This is to prevent unintended behavior when using mods that add minions with unique behavior.

This mod also has configuration to add the ability to purge grouped minions.
This is both a prevention for unintended behavior and also a feature for unique playstyles.
How this works in the configuration menu is that the minions are stored as keys in the dictionary.
Each of these keys have a corresponding value.
The value determines if the minion is a part of the group.
For example, a configuration contains a group #3531 for Stardust Dragon parts.
When a another minion is summoned and a dragon part's turn comes up for removal, all parts of the dragon will be removed.
To group minions together, assign equal values to each key minion.

# Changelog

v0.2
- Initial release.