import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/pages/admin/admin.component';
import { ElectionsComponent } from './components/pages/elections/elections.component';
import { HomeComponent } from './components/pages/home/home.component';
import { VoteComponent } from './components/pages/vote/vote.component';
import { RegisterComponent } from './components/register/register.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'vote', component: VoteComponent },
  { path: 'elections', component: ElectionsComponent },
  { path: 'admin', component: AdminComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

