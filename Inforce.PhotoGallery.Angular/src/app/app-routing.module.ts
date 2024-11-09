import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";


import { LayoutComponent } from "./components/layout/layout.component";
import { canActivate } from "./guards/auth.guard";
import { NotFoundComponent } from "./components/not-found/not-found.component";

@NgModule({
  imports: [RouterModule.forRoot([
    {
      path: '',
      component: LayoutComponent,
      children: [
        { path: '', pathMatch: "full", loadChildren: () => import('./components/albums/albums.module').then(m => m.AlbumsModule) },
        { path: 'my-albums', loadChildren: () => import('./components/my-albums/my-albums.module').then(m => m.MyAlbumsModule), canActivate: [canActivate] },
        { path: 'album', loadChildren: () => import('./components/album/album.module').then(m => m.AlbumModule) },

      ]
    },
    { path: 'login', loadChildren: () => import('./components/login/login.module').then(m => m.LoginModule) },
    { path: 'not-found', component: NotFoundComponent },
    { path: '**', redirectTo: '/not-found', pathMatch: 'full' }
  ])],
  exports: [RouterModule]
})

export class AppRoutingModule { }
