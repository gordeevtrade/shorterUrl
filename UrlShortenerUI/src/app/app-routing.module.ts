import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { UrlsComponent } from './components/urls-shortering/urls/urls.component';
import { RegistrationComponent } from './components/registration/registration/registration.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
   { path: 'shortUrl', component: UrlsComponent },
   { path: 'register', component: RegistrationComponent },
   { path: '', redirectTo: '/login', pathMatch: 'full' }, 
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
