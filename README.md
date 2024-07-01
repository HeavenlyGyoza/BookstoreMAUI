# BookstoreMAUI
<h2>Overview</h2>
<p>This project is an e-commerce application for a bookstore developed with .NET MAUI and Entity Framework Core, made to develop my knowledge and skills in multiplatform app development.</p>
<h3>Structure</h3>
<p>It consists of the .NET MAUI app, which uses the MVVM pattern; an ASP.NET Web API, currently set up to use MS SQL Server using Entity Framework Core; and a class library for the models. The app is localized to English and Spanish.</p>
<h3>TODO</h3>
<ul>
  <li>Polish UI
    <ul>
      <li>Adapt UI per platform and be fully responsive</li>
      <li>Add filtering widgets for the user's ease</li>
      <li>Fix async problems when loading data to the UI</li>
    </ul>
  </li>
  <li>Finalize wishlist system</li>
  <li>Add a shopping cart</li>
  <li>Add .NET Identity for a more robust identity system</li>
  <li>Develop unit tests</li>
  <li>Add location APIs to use when setting an order's delivery address</li>
  <li>Add categories, coupons</li>
  <li>Flesh out models</li>
  <li>Allow guests (non-registered clients) to place an order
    <ul>
      <li>Fix EF not understanding inheritance of the Client=>User/Guest models</li>
    </ul>
  </li>
  <li>Implement a system to bulk insert records into the database such as EPPlus</li>
</ul>
