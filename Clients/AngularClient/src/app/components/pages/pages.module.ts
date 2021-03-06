import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PagesRoutingModule } from './pages-routing.module';
import { HomeComponent } from './home/home.component';
import { VoteComponent } from './vote/vote.component';
import { FormsModule } from '@angular/forms';
import { ElectionsComponent } from './elections/elections.component';
import { AdminComponent } from './admin/admin.component';
import { VotedComponent } from './voted/voted.component';


@NgModule({
  declarations: [
    HomeComponent,
    VoteComponent,
    ElectionsComponent,
    AdminComponent,
    VotedComponent
  ],
  imports: [
    CommonModule,
    PagesRoutingModule,
    FormsModule
  ]
})
export class PagesModule { }
