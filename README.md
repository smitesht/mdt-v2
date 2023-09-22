# mdt-v2

### What changes?

#### 1) The code contains only thread operations without mixing tasks.
![image](https://github.com/smitesht/mdt-v2/assets/52151346/7815ed76-5281-41a4-8540-eca76135aa9e) 

#### 2)  Instead of the infinite loop  [ while(true) loops], the code used the ManualReset event, which helps the Pause/Resume loop. 
![image](https://github.com/smitesht/mdt-v2/assets/52151346/d16023e7-6ae9-4044-a2ba-eb2ceb13bfbd)

#### 3)  Instead of creating individual methods for the Generator operations, a strategy design pattern has been implemented to perform different operations.

![image](https://github.com/smitesht/mdt-v2/assets/52151346/a08b19c2-e4b4-4644-b6cd-6486bcac4800)

Used Dictionary that maps Generator operation with the defined IOperation implementations. Instead of using if/else, this map helps to query the required operation, and then perform calculations by executing the Do method.
![image](https://github.com/smitesht/mdt-v2/assets/52151346/96384bfe-fb11-419d-934b-241affd23078)


#### 4) Updated UI that contains Start and Stop buttons to Start / Stop generator operations.
![image](https://github.com/smitesht/mdt-v2/assets/52151346/8797519c-43ef-4f5d-950f-d91267158f18)

#### 5) Handle, possible error messages.

#### 6) Removed unnecessary properties/fields.



## Step One Output

![image](https://github.com/smitesht/mdt-v2/assets/52151346/cb003eca-0ad3-4acf-822c-43ca839acc9a)

## Step Two Output

![image](https://github.com/smitesht/mdt-v2/assets/52151346/8797519c-43ef-4f5d-950f-d91267158f18)

