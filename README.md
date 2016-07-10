KeePassQuickUnlock
=================================

OVERVIEW
-----
KeePassQuickUnlock is a plug-in for KeePass 2.x which lets you re-open your database without your full password.
If you want to re-open your database, you can type just a few characters to unlock quickly and easily!
If the wrong QuickUnlock password is entered, the database keeps locked and the full credentials are required to re-open.
QuickUnlock works with combined keys like password + key file, ... (but was not tested with custom plugins like OtpKeyProv).

This plugin doesn't change the way how KeePass encrypts your database so your passwords are still safe.
For more informations why this plugin doesn't break safety, read http://keepass.info/help/base/security.html#secspecattacks

INSTALLATION
-----
- Download from https://github.com/KN4CK3R/KeePassQuickUnlock/releases
- Copy the plug-in (KeePassQuickUnlock.plgx) into the KeePass program directory
- Start KeePass (and open a database)
- To enable QuickUnlock for a database you need to create a special entry in the root group of your database. The title of this entry must be "QuickUnlock". The password you set in this entry will be the password you need to provide while quick-unlocking the database. Only the title and the password field are mandatory, all other fields are ignored. QuickUnlock will not be available for your database if no entry with the title "QuickUnlock" is found in the root group or the password field is empty.

![alt tag](https://abload.de/img/quickunlock11msja.jpg)

- Close the database and re-open it
- If QuickUnlock is available for this database, QuickUnlock will be selected in the Key File providers list automaticly for you so you can enter your QuickUnlock password.

![alt tag](https://abload.de/img/quickunlock3cusel.jpg)

- To disable QuickUnlock for a database just remove the QuickUnlock entry from the root group (or don't create it).

OPTIONS
-----
By default, a QuickUnlock passwords expires after 10 minutes. If you want to change this setting, open the options form of KeePass and choose a different setting there.

![alt tag](https://abload.de/img/quickunlock2i1swq.jpg)