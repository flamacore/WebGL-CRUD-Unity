# CRUD with Unity WebGL

This is a project that aims to produce **a dynamic CRUD WebGL frontend** for MySQL (PDO) databases. Currently it's pretty rough and doesn't even handle field types. Treats all as string but with enough time, eventually it'll get updated. The reason I'm making this open source is that I hope it can get some contributions and get better with code added by them.

Honestly, **I'm a tech artist, not a coder**. Tho I do know my way around, I certainly am not as full-fledged as a dedicated software developer/engineer. That's why, the project may look very badly written - because it probably is :)

Anyways. Here it is.


### Why Unity WebGL?

For the sole reason of **it being possible**. Nothing else really! I just love working with Unity and love to see it in action other than games too. Maybe also a bit of Unity UI convenience that comes with Layout Groups and whatnot. I know it's **very far from ideal** as there are gazillions of other libraries/frameworks cut out for this exact purpose but still. Never hurts to tinker :) 

One advantage can be this being multiplatform but then I would have to also include my encrypted connection suite which I definitely won't for now :) 

### Wait, what does CRUD mean?

C = Create, R = Read, U = Update, D = Delete. 4 actions that you can do on a database. Currently, this system only supports MySQL with PDO. And I honsetly do not plan to support other databases because simply I haven't used anything other than MsSQL and MySQL. But, contributions/PR's are very welcome. Read below for more details.

## How to

Simply change your server connection settings in **UnityCRUDSettings** ScriptableObject under Assets\UnityCRUD\Scripts\Data\ScriptableObjects\Resources and change your database connection settings in **DbConn.php** under ServerFiles folder.

Server files need to reside in a folder named UnityCRUD under anywhere in your server and that place also needs to be in **UnityCRUDSettings** ScriptableObject.

Example:
Server folder structure:

 - domain.com/myawesomecrudfolder/UnityCRUD/Access.php
   domain.com/myawesomecrudfolder/UnityCRUD/EasyPDO.php
   domain.com/myawesomecrudfolder/UnityCRUD/DbConn.php

**UnityCRUDSettings** entry: domain.com/myawesomecrudfolder/

Then build your project, throw it anywhere online and run :) 

P.S. Be mindful of CORS tho. More to read: https://docs.unity3d.com/Manual/webgl-networking.html


## Third Party Notice

This repository includes the following package to enable Copy and Paste for Unity WebGL projects.
https://github.com/greggman/unity-webgl-copy-and-paste
