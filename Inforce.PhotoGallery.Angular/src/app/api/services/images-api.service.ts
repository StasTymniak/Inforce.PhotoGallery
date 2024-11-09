import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IImage } from '../../components/album/models/image';
import { IPaginatedList } from '../../models/paginated-list';

@Injectable({
  providedIn: 'root'
})
export class ImagesApiService {
  private apiUrl = `${environment.apiHost}/Images`;

  constructor(private http: HttpClient) { }

  getImages(page: number, albumId: number, pageSize: number = 5): Observable<IPaginatedList<IImage>> {
    const params = new HttpParams()
      .set('page', page)
      .set('albumId', albumId)
      .set('pageSize', pageSize);

    return this.http.get<IPaginatedList<IImage>>(`${this.apiUrl}`, { params });
  }

  reactionImage(imageId: number, isLiked: boolean): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${imageId}/user-activity`, { isLiked, });
  }

  deleteImage(imageId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${imageId}`);
  }
}
