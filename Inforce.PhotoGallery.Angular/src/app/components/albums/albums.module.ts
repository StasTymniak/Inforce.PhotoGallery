
import { NgModule } from "@angular/core";

import { AlbumsRoutingModule } from "./albums-routing.module";
import { AlbumsComponent } from "./pages/albums.component";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";

@NgModule({
  declarations: [
    AlbumsComponent
  ],
  imports: [
    AlbumsRoutingModule,
    CommonModule,
    RouterModule
  ],
  exports: [AlbumsComponent],
})

export class AlbumsModule { }
