import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AmbulanceListComponent } from './ambulance-list/ambulance-list.component';
import { WaitingEntryEditorComponent } from './waiting-entry-editor/waiting-entry-editor.component';


const routes: Routes = [
  { path: 'waiting-list', component: AmbulanceListComponent },
  { path: 'waiting-list/:id', component: WaitingEntryEditorComponent },
  {
    path: '',
    redirectTo: '/waiting-list',
    pathMatch: 'full'
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
