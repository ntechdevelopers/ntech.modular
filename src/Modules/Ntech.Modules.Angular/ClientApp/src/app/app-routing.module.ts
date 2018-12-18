import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '../../node_modules/@angular/router';
import { EventComponent } from './event/event.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  { path: 'event', component: EventComponent },
  { path: 'user', component: UserComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  declarations: [],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
