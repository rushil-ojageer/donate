# donate-prototype

This project is a work-in-progress prototype for an application that will simplify the act of giving to charities. The prototype is for a back office website where an administrative user can perform the following functions:

- Charities: A user can add, edit, or delete as a charity. A user can also view a list of charities.
- Donors: A user can add, edit, or delete as a donor. A user can also view a list of donors.
- Donor Profile: A profile of a donor can be viewed. On the profile, the following actions can be performed:
  - Link a transaction source to the donor. This transaction source will be monitored and a percentage will of each transaction will be
    donated. For the purposes of this prototype, mock transaction source data is suitable, and mock transactions will be generated.
  - Link a charity to a donor, and specifies the proportion of each donation that the charity will receive. For example, if a user donates     1% of every transaction, and is linked to two charities (Charity A will recieve 50%, and Charity B will receive 50%), then if the user 
    makes a ZAR 1000 transaction, ZAR 10 will be donated, and Charity A will receive ZAR 5 and Charity B will receive ZAR 5.

### Technology

This project uses the following technologies and frameworks:

- Frontend
  The frontend application was built using Angular 8 which connects to a microservices architected backend.

- Backend
  The backend microservices architecture was built using the following technologies:
  - DotNetCore using C# and the AspCoreNet framework with EntityFrameWorkCore
  - Microsoft SQL Server Express
  - Docker & Kubernetes
  - Rabbit MQ

### To Do's

Major technicals items still outstanding from the project:

- Authentication
- Unit Tests
- Asynchronous communication between microservices

### Demo
A demo of the project can be viewed at http://34.90.158.29
