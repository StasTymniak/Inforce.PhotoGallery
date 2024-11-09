import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AlbumComponent } from "./pages/album.component";


@NgModule({
  imports: [RouterModule.forChild([
    { path: ':id', component: AlbumComponent }
  ])],
  exports: [RouterModule]
})

export class AlbumRoutingModule { }
