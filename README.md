# FinalProjectMVC - Senior Living Resource

This project will be a resource for helping seniors and their loved ones find the right senior living community.
Once users log in or register, they will be welcomed to the homepage. From there, they will be able to navigate to other pages that 
allow them to change their preferences and browse through a list of communities that match their preferences. 

Homepage: Users are directed to this page once registration or login credentials are validated. From this page the user will be able to view information about a featured community or navigate to the other pages.

Preferences Page: Allows the user to change their community preferences at any time. Changing these will affect the results on the Browse page.

Browse Page: Lists blocks of information for each community that reflects the user's chosen preferences. Users can find out information about a community from the description and contact the community by calling the phone number that is provided or by visiting the community with the help of the Google Maps Embed API. Pictures and floor plans will be added into each block for a better user experience.

Register Page: Users are required to enter a valid username, password, and verify pasword. Email is optional. Users may also set their initial preferences on this page, or default preferences will be saved.

Login: Users will be asked to login with each visit to the Senior Living Resource. By logging in with the correct username and password combination, users will be able to browse by their preferences and make changes to their preferences. If the user is not logged in, the browse page will generate every community in the database.

Database: The Microsoft Entity Framework is employed and information about the user, the user's preferences, and the communities is stored here. The users db set and the user's preferences db set are a 1-to-1 relationship.

Languages, Frameworks and Other Technologies Used: C#, ASP.NET MVC, Razor Templates, Google Maps Embed API, HTML, CSS
