
# Inventory-System (FPS Game)
----------------------------------------------------------

This project focuses on the implementation of the Inventory system, but in this readme, the summarisation of other aspects like Player, Enemy classes is also given.

-------------------------------------------------------

# FlowCharts

## Inventory classes- 

- Items-  The items required to be collected, stored, showed and dropped back into the world space.

So, items have this kind of hierarchy:


![item-Classes-Hierarchy](https://github.com/Vimu-Sama/Vimarsh_Sharma_Inventory-System/assets/42619785/f5a4e362-00d0-431f-961a-ab563e809e98)


Items- as can be seen is the base class and conists of most of the basic functions of collectible item, but there were items like guns and if extended there can be various items like helmet, armor etc. which need to be changed in terms of physical outlook as well as will have traits associated with them when picked up and they can't be stacked, thus they are unique.

Equipable Items- it takes care of the edge cases presented when only using Items class.

-------------------------------------------------------------

-  Item Slots- These had a specific importance, because they were the building blocks of our whole Inventory system. These had the following Hierarchy:
-  
![image](https://github.com/Vimu-Sama/Vimarsh_Sharma_Inventory-System/assets/42619785/dc746359-7b37-4837-811d-c681fd167151)

