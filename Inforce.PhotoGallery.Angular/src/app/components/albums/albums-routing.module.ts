import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AlbumsComponent } from "./pages/albums.component";

@NgModule({
  imports: [RouterModule.forChild([
    { path: '', component: AlbumsComponent },
  ])],
  exports: [RouterModule]
})

export class AlbumsRoutingModule { }
