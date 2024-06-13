# Inventory-System (FPS Game)
----------------------------------------------------------

![image](https://github.com/Vimu-Sama/Vimarsh_Sharma_Inventory-System/assets/42619785/4e0e2d8c-2eb1-45d6-a371-6ff9f43e0f92)

This project focuses on the implementation of the Inventory system, but in this readme, the summarisation of other aspects like Player, Enemy classes is also given.

![Screenshot 2024-05-27 050311](https://github.com/Vimu-Sama/Vimarsh_Sharma_Inventory-System/assets/42619785/4198ee3f-db70-4218-b28a-779de2efca46)

-------------------------------------------------------

# FlowCharts

## Inventory classes- 

- Items-  The items required to be collected, stored, showed and dropped back into the world space.

So, items have this kind of hierarchy:


![item-Classes-Hierarchy](https://github.com/Vimu-Sama/Vimarsh_Sharma_Inventory-System/assets/42619785/f5a4e362-00d0-431f-961a-ab563e809e98)


1) Items- as can be seen is the base class and conists of most of the basic functions of collectible item, but there were items like guns and if extended there can be various items like helmet, armor etc. which need to be changed in terms of physical outlook as well as will have traits associated with them when picked up and they can't be stacked, thus they are unique.

2) Equipable Items- it takes care of the edge cases presented when only using Items class.

-------------------------------------------------------------

-  Item Slots- These had a specific importance, because they were the building blocks of our whole Inventory system. These had the following Hierarchy:

  
![image](https://github.com/Vimu-Sama/Vimarsh_Sharma_Inventory-System/assets/42619785/dc746359-7b37-4837-811d-c681fd167151)



Now, more on these three classes:

1)- Item Slot- Item slot is the base abstract class which was required, as there would be two types of slots, and they would have various common and uncommon attributes as well as the basic funcitonalites of item slot.

2)- Unique Item Slot- Item slots on the LHS of laboratory. They store the non-stackable items. Check image below.

![UniqueItemShow](https://github.com/Vimu-Sama/Vimarsh_Sharma_Inventory-System/assets/42619785/60a6daa9-8754-40b2-8213-3a15ba483471)

3)- Stackable Item Slot- As the name suggests they are stackable item slots, i.e. we can store more than one items in these kind of item slots.

![Stackable-ItemShowcase](https://github.com/Vimu-Sama/Vimarsh_Sharma_Inventory-System/assets/42619785/85d548ab-c15e-43e3-a74e-1f923d63d51f)

______________________

#### Thus, this was just the hierarchy, now let's see what are the different classes and how they are grouped

### Managers

![image](https://github.com/Vimu-Sama/Vimarsh_Sharma_Inventory-System/assets/42619785/6fd3cafd-9ac8-4662-91ef-d28dba1165c9)

GameManager- Manages the gameplay, and takes of small-small taks which are necessary for the level (not levels or scene) progression, including the sounds as well.

SceneManager- Manages how scenes work through the came and if they are passing on to the next properly or not.

Weapon Manager- manages the weapon- bullets and the general basic equipment.

Inventory Manager- manages the inventory and acts as a communicator for the item and item-slots(the diagram will be there at after this section)
_______________________

### Player classes

![image](https://github.com/Vimu-Sama/Vimarsh_Sharma_Inventory-System/assets/42619785/debf57c7-055a-4e6f-9eed-4482bf030a69)

Player Movement- manages player's mvoement, player's health(can be made into other class, if we are talking about scalibility)

Player Shooter- as the name implies for shooting bullets

Mouse Look- this assist in aiming and look around


## Connections- 

This image contains the whole logic and how the Inventory system classes are interacting with each other, with Inventory Manager as the backbone.

![image](https://github.com/Vimu-Sama/Vimarsh_Sharma_Inventory-System/assets/42619785/27d5dd4e-c56b-4617-896e-adf99a2e7ba1)


That's all! Thank You for reading this through.
