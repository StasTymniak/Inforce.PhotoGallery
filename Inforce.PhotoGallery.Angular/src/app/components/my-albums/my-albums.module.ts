
import { NgModule } from "@angular/core";

import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { MyAlbumsComponent } from "./pages/my-albums.component";
import { MyAlbumsRoutingModule } from "./my-albums-routing.module";
import { CreateModalComponent } from "./create-modal/create-modal.component";
import { FormsModule } from "@angular/forms";

@NgModule({
  declarations: [
    MyAlbumsComponent,
    CreateModalComponent
  ],
  imports: [
    MyAlbumsRoutingModule,

    FormsModule,
    CommonModule,
    RouterModule
  ],
  exports: [MyAlbumsComponent],
})

export class MyAlbumsModule { }
