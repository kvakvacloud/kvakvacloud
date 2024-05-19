import { navigateToUrl } from 'single-spa';
import '../../cloud-root/src/main-pr.css';
import './react-navbar.css';
import fileImage from './img/file-outline.svg';
import listImage from './img/Untitled.svg';
import settingImage from './img/setting-outline.svg';

export default function Root(props) {
  return (
    <div className="cloud-navbar flex items-center mx-auto max-w-7xl px-2">
      <div className="cloud-navbar__logo flex-shrink-0 mr-10">
        <a href="/" onClick={navigateToUrl}><img src alt="Logo"/></a>
      </div>
      <div className="cloud-navbar__menu relative h-20 flex-shrink-0">
        <ul className='flex absolute inset-y-0 left-0 items-center gap-10'>
          <li className='flex-shrink-0'><a href="/files" onClick={navigateToUrl}><img src={fileImage} alt="Файлы" width={40}/></a></li>
          <li className='flex-shrink-0'><a href="/checklist" onClick={navigateToUrl}><img src={listImage} alt="Канбан-доска"width={35}/></a></li>
          <li className='flex-shrink-0'><a href="/settings" onClick={navigateToUrl}><img src={settingImage} alt="Настройки"width={55}/></a></li>
        </ul>
      </div>
    </div>
  );
}