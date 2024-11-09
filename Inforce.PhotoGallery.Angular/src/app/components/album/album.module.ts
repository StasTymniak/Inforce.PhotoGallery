
import { NgModule } from "@angular/core";

import { AlbumComponent } from "./pages/album.component";
import { AlbumRoutingModule } from "./album-routing.module";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { ImageModalComponent } from "./image-modal/image-modal.component";

@NgModule({
  declarations: [
    AlbumComponent,
    ImageModalComponent
  ],
  imports: [
    AlbumRoutingModule,
    CommonModule,
    RouterModule
  ],
  exports: [AlbumComponent],
})

export class AlbumModule { }
