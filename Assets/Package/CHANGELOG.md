## 0.3.2 [23.12.2023]

### Added
- IUiManager binding in the UiSubInstaller installation process

## 0.3.1 [20.12.2023]

### Changed
- Fix UiButton's IsInteractable property

### Added
- UiImage - basic UiObject that wraps UnityEngine.UI.Image
- UiContentRefresh & IContentRefreshAction for performing typical UI-related operations on demand

## 0.3.0 [06.10.2023]

### Changed
- Temporarily removed all animations-related code & DOTween reference 
- ViewsHandler now tracks active views using associated Show/Hide events
- New approach for defining nested views (sub panels) in the UiView class

### Added
- UiObject - possibility to override ActivityHandler
- Add possibility to register UiViews in runtime
- Add possibility to (de)initialize UiManager externally

## 0.2.1 [04.07.2023]

### Added
- UiObject - base, abstract class for all runtime UI elements
- UI management system 
- Views handler & basic views implementation
- Tooltip handler
- DOTween-based animations