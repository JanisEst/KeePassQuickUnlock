KeePassQuickUnlock
=================================

OVERVIEW
-----
KeePassQuickUnlock is a plug-in for KeePass 2.x which lets you re-open your database without your full password.
If you want to re-open your database, you can type just a few characters to unlock quickly and easily!
If the wrong QuickUnlock key is entered, the database keeps locked and the full credentials are required to re-open.
QuickUnlock works with combined keys like password + key file, ... (but was not tested with custom plugins like OtpKeyProv).

This plugin doesn't change the way how KeePass encrypts your database so your data is still safe.
For more informations why this plugin doesn't break safety, read http://keepass.info/help/base/security.html#secspecattacks and/or [my comment here](https://github.com/JanisEst/KeePassQuickUnlock/issues/26#issuecomment-364114921).

> A note for key file users: Because of the way how KeePass works you are forced to re-provide the path to your key file anytime the QuickUnlock plugin forgets the saved credentials.
> If you want to know why there is this limitation, you can read [my comment here](https://github.com/JanisEst/KeePassQuickUnlock/issues/7#issuecomment-232715543).  

INSTALLATION
-----
- Download from [the release page](https://github.com/JanisEst/KeePassQuickUnlock/releases)
- Copy the plug-in (KeePassQuickUnlock.plgx) into the KeePass program directory
- Start KeePass (and open a database)

HOW TO USE
-----
The QuickUnlock plugin has two different modes: The default mode uses a custom entry in your database while the other mode uses a part of your master password to quickly unlock the database.

> Because of the way how KeePass works the PartOf mode works only one time before you need to provide your full password again. Because of this limitation a custom entry in the database is the preferred way to use the plugin.  
> If you want to know why there is this limitation, you can read [my comment here](https://github.com/JanisEst/KeePassQuickUnlock/issues/7#issuecomment-232715543).  
> To enable the PartOf mode see the options part of this readme.

**Custom Entry Mode**

- To enable QuickUnlock for a database you need to create a special entry in a group of the database. The title of this entry must be "QuickUnlock". The password you set in this entry will be the key you need to provide while unlocking the database. Only the title and the password field are mandatory, all other fields are ignored. QuickUnlock will not be available for the database if no entry with the title "QuickUnlock" is found or the password field is empty.

![alt tag](https://abload.de/img/quickunlock11msja.jpg)

**PartOf Mode**

- To enable QuickUnlock for a database you need to activate the PartOf mode in the options panel. The key you need to provide to unlock the database will be derived from the settings you choose there.

**independent**

- Close the database and re-open it
- If QuickUnlock is available for this database, QuickUnlock will be selected in the Key File providers list automaticly for you so you can enter your QuickUnlock key.

![alt tag](https://abload.de/img/quickunlock_keypromptdmsro.jpg)

- To disable QuickUnlock for a database just remove the QuickUnlock entry (or don't create it). If you have enabled the PartOf mode you will need to disable this mode in order to disable QuickUnlock.

OPTIONS
-----
QuickUnlock integrates itself into the KeePass settings dialog.

![alt tag](https://abload.de/img/quickunlock_optionsqgahv.jpg)

**QuickUnlock Mode**
- 'QuickUnlock' Entry only (default): Enable QuickUnlock only if the custom entry is available in a group of the database.
- 'QuickUnlock' Entry / Part of Master Password: Enable QuickUnlock if a custom entry is available or the master password satisfies the needed requirements.

**QuickUnlock Settings**
- Auto prompt (default: true): If enabled the QuickUnlock key prompt will automaticly be opened while unlocking the database if an QuickUnlock key is available.
- Valid time period (default: 10 minutes): Choose how long a saved key will be available. Once this period has expired you need to provide your full password to unlock the database again.

**Part of Master Password**
- Origin (default: Front): Choose at which end of the master password the key should be extracted.
- Length (default: 2): Choose the length of the key.

Example:
> Master Password: password  
> Settings: Front + 4 Length  
> QuickUnlock key: **pass**word
