import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { IAlbum } from '../../components/albums/models/albums.model';
import { environment } from '../../../environments/environment';
import { IPaginatedList } from '../../models/paginated-list';

@Injectable({
  providedIn: 'root'
})
export class AlbumsApiService {
  private apiUrl = `${environment.apiHost}/Albums`;

  constructor(private http: HttpClient) { }

  getAlbums(page: number, pageSize: number = 5): Observable<IPaginatedList<IAlbum>> {
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);

    return this.http.get<IPaginatedList<IAlbum>>(`${this.apiUrl}`, { params });
  }

  getAlbumById(albumId: number): Observable<IAlbum> {
    return this.http.get<IAlbum>(`${this.apiUrl}/${albumId}`);
  }

  getUserAlbums(page: number, pageSize: number = 5): Observable<IPaginatedList<IAlbum>> {
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);

    return this.http.get<IPaginatedList<IAlbum>>(`${this.apiUrl}/user`, { params });
  }

  createAlbum(albumName: string): Observable<any> {
    return this.http.post(`${this.apiUrl}`, { albumName });
  }

  deleteAlbum(albumId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${albumId}`);
  }
}
