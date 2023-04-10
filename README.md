# order-book-back
PostgeSQL install required.

Open `appsettings.json` and change connection string named "OrderBookDB" to your own.

Run migrations (`Update-Database`)

Start project with `http` preset

![image](https://user-images.githubusercontent.com/87599681/230931327-b964bf7b-04f8-486c-a68a-5660bc084bd4.png)

# order-book-front

** if folder order-book-front is empty run 'git submodule update --init'

Create file .env in root folder and add variable `NEXT_PUBLIC_API_URL = http://localhost:5126` or other link to backend.

Open terminal and run `npm i && npm run dev`.
