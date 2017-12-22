#include "pch.h"
#include "../../GameCore/CGame.h"
#include "../../GameCore/Os_Functions.h"

void CGameStateGamePlay::InitGameState()
{
	CGameState::InitGameState();

	///////
	_status = CGameStateGamePlay::enumStatusState::STATE_MENU;
	_gamePlay.Init();
	_mainMenu.Init();

	_stateTransition = 1;
	_game->_gameStateLogo.SetTransition();
#if NOTIFICATION_MAX
	Device::AskShowNotification();
#endif
}
void CGameStateGamePlay::Render(float x, float y)
{
	if (_game == NULL || _game->_GameSubState == k_INIT)
	{
		InitGameState();
	}
	if (_game->_GameSubState == k_LOOP)
	{

		_gamePlay.Render(x, y);
		_mainMenu.Render(x, y);

		if (_stateTransition == 1)
		{
			_game->_gameStateLogo.Render(x, y);
		}
	}
}

void CGameStateGamePlay::Update()
{
	if (_game->_GameSubState == k_LOOP)
	{
		if (_game->_canTouch)
		{
			if (!UpdateTouchButton())
			{
				if (_status == CGameStateGamePlay::enumStatusState::STATE_MENU)
				{
					if (_mainMenu._isRenderSetting)
					{
						if (!_mainMenu._setting.UpdateTouchButton())
						{
							if (!_mainMenu._setting._isRenderCredit)
							{
								_mainMenu._setting.UpdateTouch();
							}
						}
					}
					else if (_mainMenu._isRenderGGServices)
					{
						if (!_mainMenu._ggservices.UpdateTouchButton())
						{
							_mainMenu._ggservices.UpdateTouch();
						}
					}
					else
					{
						if (_mainMenu._shareScore._type != CShareScore::enumTypeRender::TYPE_NONE)
						{
							_mainMenu._shareScore.UpdateTouchButton();
						}
						else
						{
							if (!_mainMenu.UpdateTouchButton())
							{
								_mainMenu.UpdateTouch();
							}
						}

					}
				}
				else if (_status == CGameStateGamePlay::enumStatusState::STATE_GAME)
				{
					if (_game->countShowRateUs > 0)
					{
						_game->countShowRateUs--;
						if (_game->countShowRateUs <= 0)
						{
							_game->countShowRateUs = 0;
						}
					}
					//PDEBUG("\n _game->countShowRateUs:%d", _game->countShowRateUs);
					_gamePlay.UpdateTouch();
				}
			}
		}
		_gamePlay.Update();
		
		if (_stateTransition == 1)
		{
			if (!_game->_gameStateLogo.UpdateTransition())
			{
				_stateTransition = 0;
				_mainMenu._logoY = 0;
				_mainMenu._stateLogo = 4;
				_game->PlaySFX(SFX_WHEEL);
			}
		}
		else
		{
			_mainMenu.Update();
		}
	}
}

bool CGameStateGamePlay::UpdateTouchButton()
{
	return false;
}

void CGameStateGamePlay::UpdateTouch()
{

}

