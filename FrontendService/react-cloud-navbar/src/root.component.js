import { navigateToUrl } from 'single-spa';
import '../../cloud-root/src/main.css';
import './react-navbar.css';
import fileImage from './img/file-outline.svg';
import listImage from './img/list-check-outlien.svg';
import settingImage from './img/setting-outline.svg';

export default function Root(props) {
  return (
    <div className="cloud-navbar flex-1 mx-auto max-w-7xl px-2">
      <div className="cloud-navbar__logo"></div>
      <div className="cloud-navbar__menu relative flex h-16 items-center justify-between">
        <ul className='flex absolute inset-y-0 left-0 flex items-center'>
          <li><a href="/aaa" onClick={navigateToUrl}><img src={fileImage} alt="Files" width={45}/></a></li>
          <li><a href="/bbb" onClick={navigateToUrl}><img src={listImage} alt="Checklist"width={35}/></a></li>
          <li><a href="/ccc" onClick={navigateToUrl}><img src={settingImage} alt="Settings"width={55}/></a></li>
        </ul>
      </div>
    </div>
  );
}
