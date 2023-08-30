# [Try it out live - Keepr demo](https://keepr.qmthedev.com)

Keepr is a Picture collecting social site, users can post pictures(Keeps) and they can also create Vaults.  
Vaults can be public or private, any keep can be stored in a Vault(many to many).
each user has their own public profile which has their collection of vaults and keeps.
![image](https://github.com/Q-Mick/keepr/assets/90156237/0e341340-b047-4a82-8c73-6093a64b530c)

I opted to show Keeps in a masonry grid layout.

Deployed front and backend on an AWS utilizing, UBUNTU, Docker, .NET and NGINX

Frontend - VUE.JS (Composition API), Tailwind, JS, CSS, HTML - Demonstrated strong frontend fundamentals by creating a responsive layout for whichever device you would like to interact with the app.

Backend - Developed a robust API, with one to many and many to many relationships and user authentication via Auth0.
Backend stack - C#, MySQL(AWS RDS Database), AWS EC2, leveraged Docker containers and github actions to push production build for AWS deployment

![image](https://github.com/Q-Mick/keepr/assets/90156237/2bc89671-cb84-452d-bd2c-2f68ee307550)  

UML Reference

![image](https://github.com/Q-Mick/keepr/assets/90156237/65483280-16ac-4df6-af17-a4a742e940ec)

TODO: Add Amazon S3 Bucket for posting of clients pictures from their device rather than just supplying a link.  
TODO: Caching of the data to keep the API requests to a minimum.  
TODO: Skeleton Loaders.  
