import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';

import { CacheService } from './caching/cache.service';
import { ThemeInfo } from '../models/theme-info';
import { Theme } from '../models/theme';
import { ThemeUtils } from '../models/theme-utils';

@Injectable()
export class GalleryService {
  private readonly obj = { name: 'John' };
  private readonly galleryUri = 'assets/data/gallery.json';
  private readonly cachePrefix = 'cache_';
  private num: number;

  constructor(private http: HttpClient, private cacheService: CacheService) {
  }

  /**
   * Returns list of themes in the gallery.
   */
  public getThemesList(): Observable<ThemeInfo[]> {
    const galleryItem = this.cacheService.get(this.galleryUri);

    if (galleryItem) {
      return Observable.of(galleryItem);
    } else {
      return this.http.get<ThemeInfo[]>(this.galleryUri)
        .do(data => {
          this.cacheService.set(this.galleryUri, data, {maxAge: 7 * 24 * 60 * 60});
        });
    }
  }

  /**
   * Returns information about theme by its id.
   */
  public getThemeInfoById(id: number): Observable<ThemeInfo> {
    return this.getThemesList()
      .map(themes => {
        const result = themes.find(theme => theme.id === id);
        if (result)
          return result;

        throw new Error('Specified id not found.');
      });
  }

  /**
   * Returns theme by its information.
   */
  public getTheme(themeInfo: ThemeInfo): Observable<Theme> {
    const galleryItem = this.cacheService.get(this.cachePrefix + themeInfo.url);

    if (galleryItem) {
      const theme = ThemeUtils.fromJSON(galleryItem, themeInfo.uiTheme);
      return Observable.of(theme);
    } else {
      return this.http.get(themeInfo.url)
        .do(data => {
          this.cacheService.set(this.cachePrefix + themeInfo.url, data, {maxAge: 7 * 24 * 60 * 60});
        })
        .map(data => {
          return ThemeUtils.fromJSON(data, themeInfo.uiTheme);
        });
    }
  }
}


@Component({
  selector: 'app-readability-view',
  template: `
    <h1>Header</h1>
    <span>{{ msg }}</span>
  `,
  styles: [`
    h1 {
      display: inline;
      margin-left: 10px;
    }
    span {
      margin-left: 10px;
    }
  `]
})
export class SomeComponent {
  name = "John";
  msg = `some message ${name}`;

  @Input() age: number;
}
